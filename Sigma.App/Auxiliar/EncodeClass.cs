using System;
using System.Collections.Generic;

namespace Sigma.App.Auxiliar
{
    public class EncodeClass
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String UserName { get; set; }
        public String Id { get; set; }

        public static String EncodeString(String Id, String FirstName, String LastName, String UserName)
        {
            String code = String.Empty;

            Id = Id.Replace("-", "+");
            Id = Id.Replace("1", "ESXA");
            Id = Id.Replace("2", "JSXB");
            Id = Id.Replace("3", "PSXC");
            Id = Id.Replace("4", "EPSD");
            Id = Id.Replace("5", "JUOE");
            Id = Id.Replace("6", "JOPF");
            Id = Id.Replace("7", "JOWG");
            Id = Id.Replace("8", "SEAJ");
            Id = Id.Replace("9", "JINK");
            Id = Id.Replace("0", "JAKL");


            Id = Id.Replace("a", "AXIZ");
            Id = Id.Replace("b", "AXEX");
            Id = Id.Replace("c", "BOLA");
            Id = Id.Replace("d", "PADB");
            Id = Id.Replace("e", "PEDB");
            Id = Id.Replace("f", "ISOE");
            Id = Id.Replace("g", "JPGQ");
            Id = Id.Replace("h", "PIAR");
            Id = Id.Replace("i", "PEIT");
            Id = Id.Replace("j", "JOAQ");
            Id = Id.Replace("k", "KNOW");
            Id = Id.Replace("l", "PQXS");
            Id = Id.Replace("m", "SNSS");
            Id = Id.Replace("n", "LKSC");
            Id = Id.Replace("o", "LISS");
            Id = Id.Replace("p", "MARF");
            Id = Id.Replace("q", "MICQ");
            Id = Id.Replace("r", "FERR");
            Id = Id.Replace("s", "JULT");
            Id = Id.Replace("t", "NUDY");
            Id = Id.Replace("u", "NIDU");
            Id = Id.Replace("v", "NEDI");
            Id = Id.Replace("w", "NAOO");
            Id = Id.Replace("x", "YUOP");
            Id = Id.Replace("y", "YUPL");
            Id = Id.Replace("z", "KLCK");

            UserName = UserName.Replace("@", "PRS");
            UserName = UserName.Replace(".", "PRN");
            code += "KM1$" + Id + "OR$" + FirstName + "PR$" + LastName + "ENA$" + UserName;

            return code;
        }

        public static List<EncodeClass> DecodeString(String code)
        {
            List<EncodeClass> lst = new List<EncodeClass>();

            EncodeClass obj = new EncodeClass();
            code = code.Replace("OR$", "|");
            code = code.Replace("PR$", "|");
            code = code.Replace("ENA$", "|");

            var spl = code.Split('|');
            spl[0] = spl[0].Replace("KM1$", " ");
            spl[0] = spl[0].TrimStart();

            obj.FirstName = spl[1];
            obj.LastName = spl[2];

            obj.UserName = spl[3].Replace("PRS", "@");
            obj.UserName = obj.UserName.Replace("PRN", ".");

            obj.Id = spl[0];

            obj.Id = obj.Id.Replace("+", "-");
            obj.Id = obj.Id.Replace(" ", "-");
            obj.Id = obj.Id.Replace("ESXA", "1");
            obj.Id = obj.Id.Replace("JSXB", "2");
            obj.Id = obj.Id.Replace("PSXC", "3");
            obj.Id = obj.Id.Replace("EPSD", "4");
            obj.Id = obj.Id.Replace("JUOE", "5");
            obj.Id = obj.Id.Replace("JOPF", "6");
            obj.Id = obj.Id.Replace("JOWG", "7");
            obj.Id = obj.Id.Replace("SEAJ", "8");
            obj.Id = obj.Id.Replace("JINK", "9");
            obj.Id = obj.Id.Replace("JAKL", "0");

            obj.Id = obj.Id.Replace("AXIZ", "a");
            obj.Id = obj.Id.Replace("AXEX", "b");
            obj.Id = obj.Id.Replace("BOLA", "c");
            obj.Id = obj.Id.Replace("PADB", "d");
            obj.Id = obj.Id.Replace("PEDB", "e");
            obj.Id = obj.Id.Replace("ISOE", "f");
            obj.Id = obj.Id.Replace("JPGQ", "g");
            obj.Id = obj.Id.Replace("PIAR", "h");
            obj.Id = obj.Id.Replace("PEIT", "i");
            obj.Id = obj.Id.Replace("JOAQ", "j");
            obj.Id = obj.Id.Replace("KNOW", "k");
            obj.Id = obj.Id.Replace("PQXS", "l");
            obj.Id = obj.Id.Replace("SNSS", "m");
            obj.Id = obj.Id.Replace("LKSC", "n");
            obj.Id = obj.Id.Replace("LISS", "o");
            obj.Id = obj.Id.Replace("MARF", "p");
            obj.Id = obj.Id.Replace("MICQ", "q");
            obj.Id = obj.Id.Replace("FERR", "r");
            obj.Id = obj.Id.Replace("JULT", "s");
            obj.Id = obj.Id.Replace("NUDY", "t");
            obj.Id = obj.Id.Replace("NIDU", "u");
            obj.Id = obj.Id.Replace("NEDI", "v");
            obj.Id = obj.Id.Replace("NAOO", "w");
            obj.Id = obj.Id.Replace("YUOP", "x");
            obj.Id = obj.Id.Replace("YUPL", "y");
            obj.Id = obj.Id.Replace("KLCK", "z");


            lst.Add(obj);
            return lst;
        }

        public static String EncodeEmailString(String Id, String UserName)
        {
            UserName = UserName.ToLower();
            UserName = UserName.Replace("_", "0157");
            UserName = UserName.Replace(".", "0619");
            UserName = UserName.Replace("@", "9975");
            UserName = UserName.Replace("a", "2468");
            UserName = UserName.Replace("b", "8462");
            UserName = UserName.Replace("c", "1379");
            UserName = UserName.Replace("d", "9751");
            UserName = UserName.Replace("e", "2584");
            UserName = UserName.Replace("f", "1478");
            UserName = UserName.Replace("g", "3698");
            UserName = UserName.Replace("h", "2587");
            UserName = UserName.Replace("i", "1597");
            UserName = UserName.Replace("j", "3579");
            UserName = UserName.Replace("k", "9513");
            UserName = UserName.Replace("l", "7531");
            UserName = UserName.Replace("m", "1532");
            UserName = UserName.Replace("n", "7958");
            UserName = UserName.Replace("o", "4567");
            UserName = UserName.Replace("p", "9876");
            UserName = UserName.Replace("q", "5282");
            UserName = UserName.Replace("r", "1741");
            UserName = UserName.Replace("s", "3695");
            UserName = UserName.Replace("t", "0152");
            UserName = UserName.Replace("u", "0452");
            UserName = UserName.Replace("v", "0789");
            UserName = UserName.Replace("w", "0456");
            UserName = UserName.Replace("x", "0123");
            UserName = UserName.Replace("y", "0321");
            UserName = UserName.Replace("z", "0654");

            Id = Id.Replace("-", "+");
            Id = Id.Replace("1", "ESXA");
            Id = Id.Replace("2", "JSXB");
            Id = Id.Replace("3", "PSXC");
            Id = Id.Replace("4", "EPSD");
            Id = Id.Replace("5", "JUOE");
            Id = Id.Replace("6", "JOPF");
            Id = Id.Replace("7", "JOWG");
            Id = Id.Replace("8", "SEAJ");
            Id = Id.Replace("9", "JINK");
            Id = Id.Replace("0", "JAKL");

            String Code = "XNZ$" + Id + "UPXW$" + UserName;

            return Code;
        }

        public static List<EncodeClass> DecodeEmailString(String code)
        {
            List<EncodeClass> lst = new List<EncodeClass>();

            EncodeClass obj = new EncodeClass();
            code = code.Replace("UPXW$", "|");

            var spl = code.Split('|');
            spl[0] = spl[0].Replace("XNZ$", " ");
            spl[0] = spl[0].TrimStart();

            obj.Id = spl[0];
            obj.UserName = spl[1];

            obj.Id = obj.Id.Replace("+", "-");
            obj.Id = obj.Id.Replace(" ", "-");
            obj.Id = obj.Id.Replace("ESXA", "1");
            obj.Id = obj.Id.Replace("JSXB", "2");
            obj.Id = obj.Id.Replace("PSXC", "3");
            obj.Id = obj.Id.Replace("EPSD", "4");
            obj.Id = obj.Id.Replace("JUOE", "5");
            obj.Id = obj.Id.Replace("JOPF", "6");
            obj.Id = obj.Id.Replace("JOWG", "7");
            obj.Id = obj.Id.Replace("SEAJ", "8");
            obj.Id = obj.Id.Replace("JINK", "9");
            obj.Id = obj.Id.Replace("JAKL", "0");

            obj.UserName = obj.UserName.ToLower();

            obj.UserName = obj.UserName.Replace("0157", "_");
            obj.UserName = obj.UserName.Replace("0619", ".");
            obj.UserName = obj.UserName.Replace("9975", "@");
            obj.UserName = obj.UserName.Replace("2468", "a");
            obj.UserName = obj.UserName.Replace("8462", "b");
            obj.UserName = obj.UserName.Replace("1379", "c");
            obj.UserName = obj.UserName.Replace("9751", "d");
            obj.UserName = obj.UserName.Replace("2584", "e");
            obj.UserName = obj.UserName.Replace("1478", "f");
            obj.UserName = obj.UserName.Replace("3698", "g");
            obj.UserName = obj.UserName.Replace("2587", "h");
            obj.UserName = obj.UserName.Replace("1597", "i");
            obj.UserName = obj.UserName.Replace("3579", "j");
            obj.UserName = obj.UserName.Replace("9513", "k");
            obj.UserName = obj.UserName.Replace("7531", "l");
            obj.UserName = obj.UserName.Replace("1532", "m");
            obj.UserName = obj.UserName.Replace("7958", "n");
            obj.UserName = obj.UserName.Replace("4567", "o");
            obj.UserName = obj.UserName.Replace("9876", "p");
            obj.UserName = obj.UserName.Replace("5282", "q");
            obj.UserName = obj.UserName.Replace("1741", "r");
            obj.UserName = obj.UserName.Replace("3695", "s");
            obj.UserName = obj.UserName.Replace("0152", "t");
            obj.UserName = obj.UserName.Replace("0452", "u");
            obj.UserName = obj.UserName.Replace("0789", "v");
            obj.UserName = obj.UserName.Replace("0456", "w");
            obj.UserName = obj.UserName.Replace("0123", "x");
            obj.UserName = obj.UserName.Replace("0321", "y");
            obj.UserName = obj.UserName.Replace("0654", "z");

            lst.Add(obj);

            return lst;
        }

        public static String EncodeGUID(String ID)
        {
            ID = ID.ToLower();

            ID = ID.Replace("-", "+");
            ID = ID.Replace("1", "ESA");
            ID = ID.Replace("2", "ITA");
            ID = ID.Replace("3", "GPX");
            ID = ID.Replace("4", "RTX");
            ID = ID.Replace("5", "ATX");
            ID = ID.Replace("6", "XFX");
            ID = ID.Replace("7", "OLA");
            ID = ID.Replace("8", "HEL");
            ID = ID.Replace("9", "KIM");
            ID = ID.Replace("0", "NIK");

            ID = ID.Replace("a", "SOS");
            ID = ID.Replace("b", "ARE");
            ID = ID.Replace("c", "INO");
            ID = ID.Replace("d", "PED");
            ID = ID.Replace("e", "ETO");
            ID = ID.Replace("f", "ISO");
            ID = ID.Replace("g", "JPG");
            ID = ID.Replace("h", "PIL");
            ID = ID.Replace("i", "API");
            ID = ID.Replace("j", "JME");
            ID = ID.Replace("k", "EMP");
            ID = ID.Replace("l", "TYE");
            ID = ID.Replace("m", "SNS");
            ID = ID.Replace("n", "SNK");
            ID = ID.Replace("o", "TEK");
            ID = ID.Replace("p", "AGR");
            ID = ID.Replace("q", "MIC");
            ID = ID.Replace("r", "PSE");
            ID = ID.Replace("s", "AND");
            ID = ID.Replace("t", "JAK");
            ID = ID.Replace("u", "ORO");
            ID = ID.Replace("v", "ISI");
            ID = ID.Replace("m", "NPT");
            ID = ID.Replace("x", "TPK");
            ID = ID.Replace("y", "APT");
            ID = ID.Replace("z", "NOM");

            return ID; 
        }

        public static String DecodeGUID(String ID)
        {
            ID = ID.Replace("+"  , "-");
            ID = ID.Replace("ESA", "1");
            ID = ID.Replace("ITA", "2");
            ID = ID.Replace("GPX", "3");
            ID = ID.Replace("RTX", "4");
            ID = ID.Replace("ATX", "5");
            ID = ID.Replace("XFX", "6");
            ID = ID.Replace("OLA", "7");
            ID = ID.Replace("HEL", "8");
            ID = ID.Replace("KIM", "9");
            ID = ID.Replace("NIK", "0");

            ID = ID.Replace("SOS", "a");
            ID = ID.Replace("ARE", "b");
            ID = ID.Replace("INO", "c");
            ID = ID.Replace("PED", "d");
            ID = ID.Replace("ETO", "e");
            ID = ID.Replace("ISO", "f");
            ID = ID.Replace("JPG", "g");
            ID = ID.Replace("PIL", "h");
            ID = ID.Replace("API", "i");
            ID = ID.Replace("JME", "j");
            ID = ID.Replace("EMP", "k");
            ID = ID.Replace("TYE", "l");
            ID = ID.Replace("SNS", "m");
            ID = ID.Replace("SNK", "n");
            ID = ID.Replace("TEK", "o");
            ID = ID.Replace("AGR", "p");
            ID = ID.Replace("MIC", "q");
            ID = ID.Replace("PSE", "r");
            ID = ID.Replace("AND", "s");
            ID = ID.Replace("JAK", "t");
            ID = ID.Replace("ORO", "u");
            ID = ID.Replace("ISI", "v");
            ID = ID.Replace("NPT", "m");
            ID = ID.Replace("TPK", "x");
            ID = ID.Replace("APT", "y");
            ID = ID.Replace("NOM", "z");

            return ID;
        }
    }
}
