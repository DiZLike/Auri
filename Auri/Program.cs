using Auri.Services;

namespace Auri
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) => {
                var ex = (Exception)args.ExceptionObject;
                LogService.LogError("Unhandled exception", ex);
                MessageBox.Show($"Критическая ошибка: {ex.Message}\n\nПриложение будет закрыто.",
                    "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (!args.IsTerminating)
                    Environment.Exit(1);
            };

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, args) => {
                LogService.LogError("UI Thread exception", args.Exception);
                MessageBox.Show($"Ошибка: {args.Exception.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}