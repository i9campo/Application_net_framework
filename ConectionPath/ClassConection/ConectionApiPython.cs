namespace ConectionPath.ClassConection
{
    public static class ConectionApiPython
    {
        #region Conection Servidor.
        public static string Convertimg_tiff = "http://split.apipy.local:5000/converteimg_tiff";
        public static string Splitt_tiff = "http://split.apipy.local:5000/splitt_tiff";
        public static string Splitt_color = "http://split.apipy.local:5000/splitt_color";
        //public static string Split_Polygon = "http://split.apipy.local:5000/split_polygon";
        public static string MergeTwoBand = "http://split.apipy.local:5000/MergeTwoBands";
        #endregion

        #region Conection external
        //public static string Convertimg_tiff = "http://apipy.siccerrado.online:5000/converteimg_tiff";
        //public static string Splitt_tiff = "http://apipy.siccerrado.online:5000/splitt_tiff";
        //public static string Splitt_color = "http://apipy.siccerrado.online:5000/splitt_color";
        //public static string MergeTwoBand = "http://apipy.siccerrado.online:5000/MergeTwoBands";
        //public static string Split_Polygon = "http://apipy.siccerrado.online:5000/split_polygon";
        #endregion


        #region Conection Local. 
        //public static string Convertimg_tiff = "http://localhost:5000/converteimg_tiff";
        //public static string Splitt_tiff = "http://localhost:5000/splitt_tiff";
        //public static string Splitt_color = "http://localhost:5000/splitt_color";
        public static string Split_Polygon = "http://localhost:5000/split_polygon";
        //public static string MergeTwoBand = "http://localhost:5000/MergeTwoBands";
        #endregion
    }
}
