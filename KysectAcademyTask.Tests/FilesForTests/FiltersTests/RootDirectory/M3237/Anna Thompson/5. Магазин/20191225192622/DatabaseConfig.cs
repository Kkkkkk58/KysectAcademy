using System;
namespace LABA_5.Database.API
{
    public class DatabaseConfig
    {
        public string Server { get; set; } = @"DESKTOP-PKGKCK3\SQLEXPRESS";
        public string User { get; set; } = "Kvestus";
        public string Password { get; set; } = "123";
        public string Database { get; set; } = "itmo";
        public bool UseFile { get; set; } = false;
    }
}
