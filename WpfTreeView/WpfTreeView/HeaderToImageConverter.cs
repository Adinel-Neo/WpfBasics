using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using WpfTreeView.Directory;
using WpfTreeView.Directory.Data;

namespace WpfTreeView
{
    [ValueConversion(typeof(DirectoryItemType), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {

        public static HeaderToImageConverter Instance = new HeaderToImageConverter();


        /// <summary>
        /// Converts full path to a specific image type of a  drive, folder or file
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var image = "Imagens/file.png";
                        
            switch ((DirectoryItemType)value)
            {
                case DirectoryItemType.Drive:
                    image = "Imagens/drive.png";
                    break;
                case DirectoryItemType.Folder:
                    image = "Imagens/folder-closed.png";
                    break;
            }
            
            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
