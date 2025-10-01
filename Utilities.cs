using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vi.Extensions.Object;
using Vi.Extensions.TimeSpan;
using Vi.Extensions.SqlDataReader;
using Vi.Extensions.SqlCommand;
using Vi.Extensions.Exception;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using Vi.Extensions.String;
using Microsoft.Win32;
namespace Vi
{

    /// <summary>
    /// Provides utility methods for various operations such as hashing, string manipulation, and database interactions.
    /// </summary>
    public class Utilities
    {
        /// <summary>
        /// The Standard DAKAR blue
        /// </summary>
        public static Color BlueDakar = Color.FromArgb(59, 102, 132);


        [Obsolete("Only to show the existence of the 'Obsolete' decorator")]
        public void Obsolete() { }

        public static System.Drawing.Icon ExtractAssociatedIcon(Vi.Types.File file)
        {
            var associatedIcon = file.Exists ? System.Drawing.Icon.ExtractAssociatedIcon(file) : SystemIcons.Error;
            return associatedIcon;
        }

        /// <summary>
        /// Gets the HASH SHA256 associated with a file
        /// </summary>
        /// <param name="fullFileName">The fully qualified file name.</param>
        /// <returns>An HASH code</returns>
        public static byte[] GetHASH(Vi.Types.File fullFileName)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                using (var stream = System.IO.File.OpenRead(fullFileName))
                {
                    byte[] buffer = new byte[4096]; // Dimensione del buffer
                    int bytesRead;

                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        sha256.TransformBlock(buffer, 0, bytesRead, null, 0);
                    }
                    sha256.TransformFinalBlock(buffer, 0, 0); // Completa il calcolo dell'hash

                    return sha256.Hash;

                }
            }
        }

        // The setup project put all the executables for this solution in the same folder.
        // This way (adding the project nale to the path), we're creating dedicated
        // 'spaces' for each application again."
        /// <summary>
        /// Gets the IO.Path.Combine(Vi.Utility.AppRoot, "settings.ini")
        /// </summary>
        public static Vi.Types.File GetSettingsINI(string subFolderName)
        {

            var fileName = "settings.ini";
            var subPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, subFolderName);
            subPath.ToDirectory().Create();
            var settingsINI = System.IO.Path.Combine(subPath, fileName);

            // =========================================================================== //
            /*
            // Don't make this run will create an stack overflow
            // Do not delete this code: it is a reminder!!!!
            if (!settingsINI.ToFile().Exists)
            {
                Vi.Logger.Warn($"File {fileName} not found: {settingsINI}");
            }
            */
            // =========================================================================== //

            return settingsINI;
        }

        /// <summary>
        /// The standar path for the images (The folder 'Images. under 'AppDomain.CurrentDomain.BaseDirectory'
        /// </summary>
        public static Vi.Types.Directory ImagesPath
        {

            get
            {
                var imagesPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                imagesPath.ToDirectory().Create();
                // =========================================================================== //
                /*
                // Don't make this run will create an stack overflow
                // Do not delete this code: it is a reminder!!!!
                if (!settingsINI.ToFile().Exists)
                {
                    Vi.Logger.Warn($"File {fileName} not found: {settingsINI}");
                }
                */
                // =========================================================================== //

                return imagesPath;
            }
        }

        /// <summary>
        /// Una variante del Metodo 'String.Join' Converte il parametro 'value' in una stringa di valori separati da ';'  
        /// /// </summary>
        /// <param name="values">Lista di oggetti da mettere in sequenza. Di ognuno viene convertito in stringa e vengono rimosse le enetuali occorrenze di ';'. Si esegue  'value[x].ToString().Replace(";", " ").</param>
        /// <returns>Una sequenza di stringhe separate da ' ; ' (compresi gli spazi). 'null' viene convertito in 'Empty'.</returns>
        public static string Join(params object[] values)
        {
            var strings = new string[values.Length];
            for (int index = 0; index < values.Length; index++)
            {
                var value = values[index];

                var item =
                    (value.IsNull()) ? String.Empty :
                    (value is DateTime) ? ((DateTime)value).ToString("dd/MM/yyyy") :
                    (value is TimeSpan) ? ((TimeSpan)value).ToHHMM() :
                    String.Empty + value.ToString();

                strings[index] = item.Trim().Replace(";", " ");
            }

            return String.Join("; ", strings);

        }

        /// <summary>
        /// Executes a SQL query and processes the result using the provided callbacks. 
        /// This decouples the connection with the data manipulation.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="sql">The SQL query to execute.</param>
        /// <param name="row">A callback function to process each row of the result set. Returns true to continue reading, false to stop.</param>
        /// <param name="header">A callback action to process the header of the result set.</param>
        /// <param name="onException">A callback action to handle exceptions that occur during execution.</param>
        public static void ExecuteReader(string connectionString, string sql, Func<DbDataReader, bool> row, Action<DbDataReader> header, Action<System.Exception> onException)
        {
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    //connection.ConnectionString = connectionString;
                    //var connectionTimeout = connection.ConnectionTimeout;

                    connection.Open();

                    DbCommand command = connection.CreateCommand();
                    command.CommandText = sql;
                    command.CommandTimeout = 3;// secondi.

                    using (DbDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (header != null) { header(reader); }
                            if (row != null)
                            {
                                var continua = true;
                                while (continua && reader.Read())
                                {
                                    continua = row(reader);
                                }
                            }
                        }
                        else
                        {
                            onException(new System.Exception("Nessun dato da visualizzare."));
                        }
                    }
                }
            }
            catch (System.Exception se)
            {
                if (onException != null) { onException(se); }
            }
        }

        /// <summary>
        /// Executes a SQL query asynchronously and processes the result using the provided callbacks. 
        /// This decouples the connection with the data manipulation.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="sql">The SQL query to execute.</param>
        /// <param name="row">A callback function to process each row of the result set. Returns true to continue reading, false to stop.</param>
        /// <param name="header">A callback action to process the header of the result set.</param>
        /// <param name="onException">A callback action to handle exceptions that occur during execution.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public static async Task ExecuteReaderAsync(string connectionString, string sql, Func<DbDataReader, bool> row, Action<DbDataReader> header, Action<System.Exception> onException)
        {
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    //connection.ConnectionString = connectionString;
                    //var connectionTimeout = connection.ConnectionTimeout;

                    await connection.OpenAsync();

                    DbCommand command = connection.CreateCommand();
                    command.CommandText = sql;
                    command.CommandTimeout = 3;// secondi.

                    using (DbDataReader reader = await command.ExecuteReaderAsync(System.Data.CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (header != null) { header(reader); }
                            if (row != null)
                            {
                                var continua = true;
                                while (continua && await reader.ReadAsync())
                                {
                                    continua = row(reader);
                                }
                            }
                        }
                        else
                        {
                            onException(new System.Exception("Nessun dato da visualizzare."));
                        }
                    }
                }
            }
            catch (System.Exception se)
            {
                if (onException != null) { onException(se); }
            }
        }

        /// <summary>
        /// Executes a SQL query asynchronously and fills a DataTable with the result.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="sql">The SQL query to execute.</param>
        /// <param name="onSuccess">A callback action to process the filled DataTable.</param>
        /// <param name="onException">A callback action to handle exceptions that occur during execution.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public static async Task FillAsync(string connectionString, string sql, Action<DataTable> onSuccess, Action<System.Exception> onException)
        {
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    //connection.ConnectionString = connectionString;
                    //var connectionTimeout = connection.ConnectionTimeout;

                    await connection.OpenAsync();

                    DbCommand command = connection.CreateCommand();
                    command.CommandText = sql;
                    command.CommandTimeout = 3;// secondi.

                    using (var da = new System.Data.SqlClient.SqlDataAdapter(sql, connection))
                    {
                        // 3
                        // Use DataAdapter to fill DataTable
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        // 4
                        // Render data onto the screen
                        //return dt;

                        if (onSuccess != null) { onSuccess(dt); }
                    }
                }
            }
            catch (System.Exception se)
            {
                if (onException != null) { onException(se); }
                //return new DataTable();
            }
        }

        /// <summary>
        /// Check the correctness of the parameter 'section'
        /// </summary>
        /// <param name="value">The string to check. (Can't be: null; empty; spaces.)</param>
        /// <param name="onFeedBack">The callback used to manage exceptions.</param>
        /// <returns>True if 'section' passes the checks. False otherwise and the callback 'onWarning' is called also.</returns>
        public static bool CheckParameter(string value, Action<string> onFeedBack)
        {
            if (value == null)
            {
                onFeedBack?.Invoke("is null.");
                return false;
            }

            if (("" + value) == "")
            {
                onFeedBack?.Invoke("is the empty string.");
                return false;
            }

            if (value.Trim() == "")
            {
                onFeedBack?.Invoke("is a string of spaces.");
                return false;
            }

            return true;
        }


        public static Bitmap GetImage(string fileName, float opacity)
        {
            try
            {
                var fullFileName = System.IO.Path.Combine(Vi.Utilities.ImagesPath, fileName);
                // Load the image from the file
                using (Image image = Image.FromFile(fullFileName))
                {
                    var transparentImage = Vi.Utilities.GetImage(image, opacity);

                    // Add the transparent image to the ImageList
                    //imlTreeView.Images.Add(transparentImage);
                    return transparentImage;
                }
            }
            catch (System.Exception se)
            {
                se.Log();
                return SystemIcons.Error.ToBitmap();

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="opacity"></param>
        /// <returns></returns>
        public static Bitmap GetImage(System.Drawing.Image image, float opacity)
        {

            // Create a new bitmap with the same size as the original image
            Bitmap transparentImage = new Bitmap(image.Width, image.Height);

            // Create a graphics object to draw the image
            using (Graphics g = Graphics.FromImage(transparentImage))
            {
                // Set the opacity level (50% transparent)
                ColorMatrix colorMatrix = new ColorMatrix();
                colorMatrix.Matrix33 = opacity; // 0.99f; // Set alpha to 50%

                // Create an image attributes object and set the color matrix
                using (ImageAttributes attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(colorMatrix);
                    g.DrawImage(
                        image,
                        new System.Drawing.Rectangle(0, 0, transparentImage.Width, transparentImage.Height),
                        0, 0,
                        image.Width, image.Height,
                        GraphicsUnit.Pixel,
                        attributes
                    );
                }
            }

            // Add the transparent image to the ImageList
            //imlTreeView.Images.Add(transparentImage);
            return transparentImage;
        }

        public static Bitmap GetImageGray(string fileName, float opacity)
        {
            try
            {
                var fullFileName = System.IO.Path.Combine(Vi.Utilities.ImagesPath, fileName);
                // Carica l'immagine dal file
                using (Image originalImage = Image.FromFile(fullFileName))
                {
                    var grayscaleImage = Vi.Utilities.GetImageGray(originalImage, opacity);

                    // Aggiungi l'immagine in scala di grigi all'ImageList
                    return grayscaleImage;
                }

            }
            catch (System.Exception se)
            {
                se.Log();
                return SystemIcons.Error.ToBitmap();

            }
        }

        /// <summary>
        /// Converts the provided image in gray scale
        /// </summary>
        /// <param name="originalImage">the image to convert</param>
        /// <param name="opacity">The desidered opacity</param>
        /// <returns>Returns the provided imaged grayed and opaque</returns>
        public static Bitmap GetImageGray(System.Drawing.Image originalImage, float opacity)
        {
            try
            {
                // Crea un nuovo bitmap con le stesse dimensioni dell'immagine originale
                Bitmap grayscaleImage = new Bitmap(originalImage.Width, originalImage.Height);

                // Crea un oggetto graphics per disegnare l'immagine
                using (Graphics g = Graphics.FromImage(grayscaleImage))
                {
                    // Crea un oggetto ImageAttributes e imposta la matrice dei colori per la scala di grigi
                    using (ImageAttributes attributes = new ImageAttributes())
                    {
                        ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                        {
            new float[] { 0.3f, 0.3f, 0.3f, 0, 0 }, //. . . . Rosso
            new float[] { 0.59f, 0.59f, 0.59f, 0, 0 }, // . . Verde
            new float[] { 0.11f, 0.11f, 0.11f, 0, 0 }, // . . Blu
            new float[] { 0, 0, 0, opacity, 0 }, // . . . . . Alpha
            new float[] { 0, 0, 0, 0, opacity }  // . . . . . Trasparenza
                        });

                        attributes.SetColorMatrix(colorMatrix);
                        g.DrawImage(
                            originalImage,
                            new System.Drawing.Rectangle(0, 0, grayscaleImage.Width, grayscaleImage.Height),
                            0, 0,
                            originalImage.Width, originalImage.Height,
                            GraphicsUnit.Pixel,
                            attributes
                        );
                    }
                }

                // Aggiungi l'immagine in scala di grigi all'ImageList
                return grayscaleImage;

            }
            catch (System.Exception se)
            {
                se.Log();
                return SystemIcons.Error.ToBitmap();

            }

        }

        /// <summary>
        /// Overlaps 2 images
        /// </summary>
        /// <param name="background">The background image</param>
        /// <param name="imageOnTop">The image on top</param>
        /// <returns>The overlay of the 2 images.</returns>
        public static Bitmap OverlayImages(Image background, Image imageOnTop)
        {
            try
            {
                // Crea un nuovo bitmap con le stesse dimensioni delle immagini
                Bitmap overlayImage = new Bitmap(background.Width, background.Height);

                // Crea un oggetto graphics per disegnare le immagini
                using (Graphics g = Graphics.FromImage(overlayImage))
                {
                    // Disegna la prima immagine
                    g.DrawImage(background, new System.Drawing.Rectangle(0, 0, overlayImage.Width, overlayImage.Height));

                    // Disegna la seconda immagine sopra la prima
                    g.DrawImage(imageOnTop, new System.Drawing.Rectangle(overlayImage.Width - imageOnTop.Width, overlayImage.Height - imageOnTop.Height, imageOnTop.Width, imageOnTop.Height));
                }

                // Aggiungi l'immagine sovrapposta all'ImageList
                // imlTreeView.Images.Add(overlayImage);
                return overlayImage;

            }
            catch (System.Exception se)
            {
                se.Log();
                return SystemIcons.Error.ToBitmap();

            }
        }

        public static Bitmap OverlayImages(string fileName1, string fileName2)
        {
            try
            {
                // Carica le immagini dal file
                var fullFileName1 = System.IO.Path.Combine(Vi.Utilities.ImagesPath, fileName1);
                var fullFileName2 = System.IO.Path.Combine(Vi.Utilities.ImagesPath, fileName2);
                using (Image firstImage = Image.FromFile(fullFileName1))
                using (Image secondImage = Image.FromFile(fullFileName2))
                {
                    // Aggiungi l'immagine sovrapposta all'ImageList
                    //imlTreeView.Images.Add(overlayImage);
                    return OverlayImages(firstImage, secondImage);
                }

            }
            catch (System.Exception se)
            {
                se.Log();
                return SystemIcons.Error.ToBitmap();

            }
        }

        public static Bitmap GetRedDot(int diameter, int opacity)
        {
            try
            {
                if (diameter <= 0)
                {
                    return new Bitmap(1, 1);
                }

                Bitmap bmp = new Bitmap(diameter, diameter);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.None;
                    g.Clear(Color.Transparent);

                    opacity = Math.Max(0, Math.Min(255, opacity));

                    int alpha = opacity;

                    // Crea un colore rosso con l'opacità specificata
                    Color redWithOpacity = Color.FromArgb(alpha, Color.Red);

                    using (SolidBrush brush = new SolidBrush(redWithOpacity))
                    {
                        g.FillEllipse(brush, 0, 0, diameter - 1, diameter - 1);
                    }
                }
                return bmp;

            }
            catch (System.Exception se)
            {
                se.Log();
                return SystemIcons.Error.ToBitmap();

            }
        }

        /*
        /// <summary>
        /// Retrieves the associated icon for a given file extension from the Windows registry.
        /// </summary>
        /// <param name="fileExtension">The file extension</param>
        /// <returns>The ico associated with the extension if any, null otherwise.</returns>
        public Icon GetAssociatedIcon(string fileExtension)
        {
            string iconPath = string.Empty;

            // Accedi alla chiave di registro per l'estensione del file
            //using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(fileExtension))
            using (RegistryKey key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(fileExtension))
            {
                if (key != null)
                {
                    // Ottieni il valore della chiave DefaultIcon
                    using (RegistryKey iconKey = key.OpenSubKey(fileExtension))
                    {
                        if (iconKey != null)
                        {
                            iconPath = iconKey.GetValue(string.Empty)?.ToString();
                        }
                    }
                }
            }

            // Estrai l'icona dal percorso
            if (!string.IsNullOrEmpty(iconPath))
            {
                return Icon.ExtractAssociatedIcon(iconPath);
            }

            return null; // Returns null there isn't any icon associated to the extension or the extension is not valid.
        }
        */

        /// <summary>
        /// Generates a transparent bitmap containing a red dot in the bottom right corner.
        /// </summary>
        /// <returns>A bitmap 48x48 with a red dot image 8 pixel radius, 12 pixel from bottom 12 pixel from right with 50% opacity.</returns>
        private Bitmap GetRedDot()
        {
            int width = 48;
            int height = 48;
            int circleDiameter = 16;
            float opacity = 0.5f; // 50% trasparenza

            using (Bitmap bmp = new Bitmap(width, height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // Rendi lo sfondo trasparente
                    g.Clear(Color.Transparent);

                    // Calcola la posizione del cerchio
                    int centerX = width - 12; // 12 px da destra
                    int centerY = height - 12; // 12 px dal fondo
                    int circleX = centerX - (circleDiameter / 2);
                    int circleY = centerY - (circleDiameter / 2);

                    // Crea un pennello rosso semi-trasparente
                    Color redWithOpacity = Color.FromArgb((int)(opacity * 255), Color.Red);
                    using (SolidBrush brush = new SolidBrush(redWithOpacity))
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.FillEllipse(brush, circleX, circleY, circleDiameter, circleDiameter);
                    }
                }

                // Salva o usa la bitmap come preferisci
                //bmp.Save("red_circle.png", System.Drawing.Imaging.ImageFormat.Png);
                //Console.WriteLine("Immagine salvata come red_circle.png");
                return bmp;
            }
        }

    }
}

////////////public static Bitmap GetRedDot(int diameter, decimal opacity)
////////////{
////////////    if (diameter <= 0)
////////////    {
////////////        return new Bitmap(1, 1); // Restituisce una bitmap minima per diametri non validi
////////////    }

////////////    Bitmap bmp = new Bitmap(diameter, diameter);
////////////    using (Graphics g = Graphics.FromImage(bmp))
////////////    {
////////////        g.SmoothingMode = SmoothingMode.None;
////////////        g.Clear(Color.Transparent); // Rende lo sfondo trasparente

////////////        using (SolidBrush brush = new SolidBrush(Color.Red))
////////////        {
////////////            g.FillEllipse(brush, 0, 0, diameter - 1, diameter - 1);
////////////        }
////////////    }
////////////    return bmp;
////////////}

/////////////// <summary>
/////////////// retrieves the root for the application: it is 'Path.Combine(BaseDirectory, assemblyName)
/////////////// </summary>
////////////public static Vi.Types.Directory GetAppRoot()
////////////{
////////////    get
////////////    {
////////////        var appRoot = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
////////////        // =========================================================================== //
////////////        /*
////////////        // Don't make this run will create an stack overflow
////////////        // Do not delete this code: it is a reminder!!!!
////////////        if (!appRoot.ToDirectory().Exists)
////////////        {
////////////            Vi.Logger.Warn($"Root Directory for the applcation not found: {appRoot}");
////////////        }
////////////        */
////////////        // =========================================================================== //

////////////        return appRoot;
////////////    }
////////////}