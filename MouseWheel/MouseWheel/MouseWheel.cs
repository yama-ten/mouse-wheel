using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Windows.Forms;
//using CSUACSelfElevation;
using Microsoft.Win32;


/// <summary>
/// マウスホイールのスクロール方向を切り替えるツール
/// Windows7,10 兼用
/// 2016.02.14:Windows7用
/// 2016.08.21:Windows10用
/// 2018.11.18 いろいろ改造
/// </summary>
namespace MouseWheel
{
	public partial class MouseWheel : Form
	{
		public struct mouse_stat
		{
			public string Name;
			public uint FFWheel;
			public uint WWEnable;

			public mouse_stat(string name, uint ff, uint ww)
			{
				Name = name;
				FFWheel = ff;
				WWEnable = ww;
			}
		}

		public readonly string REG_WINNT_VER = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";
        public readonly string REG_ENUM_HID = @"SYSTEM\CurrentControlSet\Enum\HID\";
        public readonly string REG_HID = @"\HID\";
		public readonly string NL = "\r\n";

		public readonly string CUR_VER = "CurrentVersion";
		public readonly string DEV_PARAMS = "Device Parameters";
		public readonly string KEY_FF_WHEEL = "FlipFlopWheel";
		public readonly string KEY_WW_ENABLE = "WaitWakeEnabled";

		public Dictionary<string, mouse_stat> MouseStart = new Dictionary<string, mouse_stat>();
		public string CurrentVersion = null;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MouseWheel(string[] s)
		{
			InitializeComponent();

			RegistryKey regkey = Registry.LocalMachine.OpenSubKey(REG_WINNT_VER);
			Dictionary<string, string> key_val = new Dictionary<string, string>();
			foreach (string name in regkey.GetValueNames())
				key_val[name] = regkey.GetValue(name).ToString();

			CurrentVersion = key_val[CUR_VER];

#if DEBUG
			foreach (string name in regkey.GetValueNames())
				System.Diagnostics.Debug.WriteLine(name+" = "+regkey.GetValue(name));
#endif
			this.Text = REG_ENUM_HID;
			tbox_verinfo.Text
				= "CurrentVersion : " + CurrentVersion + NL
				+ "ProductName : " + key_val["ProductName"] + NL
				+ "CurrentBuild : " + key_val["CurrentBuild"];
			listView1.Columns[0].Text = "Hardware ID (include MOUSE " + DEV_PARAMS + ")";
			listView1.Columns[1].Text = KEY_FF_WHEEL;
			listView1.Columns[2].Text = KEY_WW_ENABLE;

			SetListView();
		}

		/// <summary>
		/// マウスホイールに関するレジストリの内容をリストに表示。
		/// </summary>
		public void SetListView()
		{
            if (listView1.Items.Count > 0)
                listView1.Items.Clear();

			FindAndSetList(REG_ENUM_HID);

			foreach (KeyValuePair<string, mouse_stat> reg in MouseStart) {
				mouse_stat ms = reg.Value;

				ListViewItem lvi = listView1.Items.Add(ms.Name);
				lvi.UseItemStyleForSubItems = false;
				lvi.SubItems[0].Name = DEV_PARAMS;
				lvi.SubItems[0].Tag = (object)ms;

				lvi.SubItems.Add((ms.FFWheel == 0) ? "Win" : "Mac");
				lvi.SubItems[1].Name = KEY_FF_WHEEL;
				lvi.SubItems[1].Tag = (object)ms.FFWheel;
				lvi.SubItems[1].ForeColor = (ms.FFWheel == 0) ? Color.Gray : Color.DeepSkyBlue;

				lvi.SubItems.Add((ms.WWEnable == 0) ? "Disable" : "Enable");
				lvi.SubItems[2].Name = KEY_WW_ENABLE;
				lvi.SubItems[2].Tag = (object)ms.WWEnable;
				lvi.SubItems[2].ForeColor = (ms.WWEnable == 0) ? Color.Green : Color.DarkRed;

			}
		}

		/// <summary>
		/// サブキー"Device Parameters" の "FlipFlopWheel & WaitWakeEnabled" の値を調べる。
		/// </summary>
		/// <param name="VidKey"></param>
		/// <param name="DevKey"></param>
		/// <param name="key_name"></param>
		/// <param name="key_val"></param>
		void getValue_FF_and_WW(RegistryKey VidKey, string sub_name, string key_name, string key_val)
		{
			RegistryKey DevKey = VidKey.OpenSubKey(sub_name);

			string val = (string)DevKey.GetValue(key_name, null);
			if (string.Compare(val, key_val, true) == 0) {
				try {
					RegistryKey para_key = DevKey.OpenSubKey(DEV_PARAMS);

					// ("Device Parameters" 付き)
					string param_name = para_key.Name.Substring(para_key.Name.IndexOf(REG_HID) + 5);

					// なまえ ("Device Parameters"なし)
					string name = DevKey.Name.Substring(VidKey.Name.IndexOf(REG_HID) + 5);

					// マウスホイールの向き FlipFlopWheel
					uint ff_wheel = Convert.ToUInt32(para_key.GetValue(KEY_FF_WHEEL, 0));

					// 電源管理の有無 WaitWakeEnabled
					uint ww_enable = Convert.ToUInt32(para_key.GetValue(KEY_WW_ENABLE, 0));

					MouseStart.Add(param_name, new mouse_stat(name, ff_wheel, ww_enable));
#if DEBUG
			System.Diagnostics.Debug.WriteLine(param_name +" - "+ name + " = " + ff_wheel + ","+ww_enable);
#endif
					para_key.Close();
				}
				catch (Exception er) {
					MessageBox.Show(er.Message + "\r\nGetVal(Device Parameters)", "ERROR:4");
				}
			}

			DevKey.Close();
		}

		/// <summary>
		/// レジストリを走査してマウスホイールの情報をしらべる。
		/// </summary>
		/// <param name="key"></param>
		private void FindAndSetList(string key)
		{
			MouseStart = new Dictionary<string, mouse_stat>();

			// 調べるレジストリツリーの先頭
			// HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\HID
			try {
				RegistryKey EnumHID = Registry.LocalMachine.OpenSubKey(key);

				// サブキーを調べる	...\\HID\VID_xxx
				foreach (string vid in EnumHID.GetSubKeyNames()) {	
					try {
						RegistryKey VidKey = EnumHID.OpenSubKey(vid, 
							RegistryKeyPermissionCheck.ReadSubTree,
							RegistryRights.ReadKey | RegistryRights.QueryValues);

						// その下のデバイスのキーを調べる。
						foreach (string sub_name in VidKey.GetSubKeyNames()) {
							// マウスかどうかの判定と値取得
							switch (CurrentVersion) {
							case "6.3":	// Win10 は "Service" == "mouhid" になっているかで調べる。
								getValue_FF_and_WW(VidKey, sub_name, "Service", "mouhid");
								break;

							case "6.1": // Win7 は "Class" == "Mouse" で調べる。
							default:    // 他は知らん
								getValue_FF_and_WW(VidKey, sub_name, "Class", "Mouse");
								break;
							}
						}
						VidKey.Close();
					}
					catch (Exception er) {
						MessageBox.Show(er.Message + "\r\nHID\\VID_xxx", "ERROR:2");
					}
				}
				EnumHID.Close();
			}
			catch (Exception er) {
				MessageBox.Show(er.Message + "\r\nEnum\\HID", "ERROR:1");
			}

			return;
		}

        /// <summary>
        /// [Win]ボタンの処理。
        /// Device Parameters = 0 にする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDirWin_Click(object sender, EventArgs e)
		{
			toggle_all_direction(0U);
			SetListView();
		}

		/// <summary>
		/// [Mac]ボタンの処理。
		/// Device Parameters = 1 にする。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDirMac_Click(object sender, EventArgs e)
		{
			toggle_all_direction(1U);
			SetListView();
		}

		/// <summary>
		/// リストの全部切り替え
		/// </summary>
		/// <param name="sw"></param>
		public void toggle_all_direction(uint sw)
		{
			foreach (KeyValuePair<string, mouse_stat> reg in MouseStart)
				toggle_device_param(reg.Key, KEY_FF_WHEEL, sw);
		}

		/// <summary>
		/// リストの全部切り替え
		/// </summary>
		/// <param name="sw"></param>
		public void toggle_all_waitwake(uint sw)
		{
			foreach (KeyValuePair<string, mouse_stat> reg in MouseStart)
				toggle_device_param(reg.Key, KEY_WW_ENABLE, sw);
		}
		
		/// <summary>
		/// 一個だけの切り替え
		/// </summary>
		/// <param name="key"></param>
		/// <param name="val"></param>
		private void toggle_device_param(string key, string param, uint val)
		{
			try {
				string subkey = System.IO.Path.Combine(REG_ENUM_HID, key);
			System.Diagnostics.Debug.WriteLine(subkey+"="+val);

				RegistryKey reg_key = Registry.LocalMachine.OpenSubKey(subkey, true);
				reg_key.SetValue(param, val, RegistryValueKind.DWord);
				reg_key.Close();
			}
			catch (Exception er) {
				MessageBox.Show(er.Message, "ERROR");
			}
		}

		/// <summary>
		/// ダブルクリックされた行のホイル方向を切り替え
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			string subkey = null;

			foreach (ListViewItem lvi in listView1.Items) {
				if (lvi.Selected) {
					subkey = System.IO.Path.Combine(lvi.Text, DEV_PARAMS);
					uint sw = 1U - (uint)lvi.SubItems[1].Tag;
					toggle_device_param(subkey, KEY_FF_WHEEL, sw);
					break;
				}
			}
			SetListView();
		}


		/// <summary>
		/// オプションで -view 使ったとき
		/// </summary>
		public void view_all_direction()
		{
			string msg = "";
			foreach (KeyValuePair<string, mouse_stat> reg in MouseStart)
			{
				mouse_stat ms = reg.Value;
				msg += ms.Name + NL
					+ "\\" + DEV_PARAMS + " : "
					+ KEY_FF_WHEEL + "=" + ms.FFWheel + ", "
					+ KEY_WW_ENABLE + "=" + ms.WWEnable + NL + NL;
			}
			MessageBox.Show(msg, KEY_FF_WHEEL);
		}

		public void view_help()
		{
			string msg = @"レジストリを書き換えてマウスホイールのスクロール方向を変えます。

コマンドライン 一括操作
 -m | /mac ---- Mac風のホイールの動作になります。
 -w | /win ---- Windowsのホイールの動作に戻します。
 -e | /enable ---- マウスでスリープが解除されます。
 -d | /disable ---- マウスを動かしてもスリープしたままにできます。
 -h | /? ---- この窓が出ます。
 ※ Windows8(ver6.2)系は近くにないので試していません。
";
			MessageBox.Show(msg, KEY_FF_WHEEL);
		}


		/// <summary>
		/// ヘルプボタンをつけてみた
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MouseWheel_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
		{
			view_help();
			e.Cancel = true; //	?Cursor を消す。
		}
	}
}
