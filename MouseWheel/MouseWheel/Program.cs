using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MouseWheel
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			MouseWheel form = new MouseWheel(null);

			foreach (string s in args) {
				switch (s.ToLower()) {
				default:
				case "-h":
				case "/?":
					form.view_help();
					return;

				case "-v":
				case "/v":
				case "-view":
				case "/view":
					form.view_all_direction();
					return;

				case "-w":
				case "/w":
				case "-win":
				case "/win":
					form.toggle_all_direction(0U);
					break;

				case "-m":
				case "/m":
				case "-mac":
				case "/mac":
					form.toggle_all_direction(1U);
					break;

				case "-e":
				case "/e":
				case "-enable":
				case "/enable":
					form.toggle_all_waitwake(1U);
					break;

				case "-d":
				case "/d":
				case "-disable":
				case "/disable":
					form.toggle_all_waitwake(0U);
					break;
				}
			}
			form.SetListView();

			for (int i=0; i<args.Length; i++)
				System.Diagnostics.Debug.WriteLine("args["+i+"]"+args[i]);

			Application.Run(form);
		}
	}
}
