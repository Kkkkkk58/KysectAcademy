using System;

namespace LABA_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = new Controller();
            controller.SetSongs();
            var catalog = controller.Catalog;

            var search1 = catalog.Search("Conan");
            var search2 = catalog.Search("Conan", "ConanFirst");
            var search3 = catalog.Search("Conan", "ConanFirst", "2");
            var search4 = catalog.Search("Conan", "ConanFirst", "2", "Rap");
            var search5 = catalog.Search("Conan", "ConanFirst", null, "Rap");
            var search6 = catalog.Search("Conan", null, null, "Rap");
            var search7 = catalog.Search(null, null, null, "Rap");
            var search8 = catalog.Search(null, null, "2", null);
            var search9 = catalog.Search(null, "ConanFirst", "2", null);
            var search10 = catalog.Search(null, "ConanFirst", null, null);
            var search11 = catalog.Search("noname");
            var search12 = catalog.Search();

            Console.ReadLine();
        }
    }
}
