//------------------------------------------------------
// DirectoryTreeViewItem.cs (c) 2006 by Charles Petzold
//------------------------------------------------------
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Petzold.RecurseDirectoriesIncrementally
{
    public class DirectoryTreeViewItem: ImagedTreeViewItem
    {
        DirectoryInfo dir;

        // Constructor requires DirectoryInfo object.
        //Для конструктора требуется объект DirectoryInfo.
        public DirectoryTreeViewItem(DirectoryInfo dir)
        {
            this.dir = dir;
            Text = dir.Name;

            SelectedImage = new BitmapImage(
                new Uri("pack://application:,,/Images/OPENFOLD.BMP"));

            UnselectedImage = new BitmapImage(
                new Uri("pack://application:,,/Images/CLSDFOLD.BMP"));
        }
        // Public property to obtain DirectoryInfo object.
        //Открытое свойство для получения объекта DirectoryInfo.
        public DirectoryInfo DirectoryInfo
        {
            get { return dir; }
        }
        // Public method to populate with items.
        //Открытый метод для заполнения элементами.
        public void Populate()
        {
            DirectoryInfo[] dirs;

            try
            {
                dirs = dir.GetDirectories();       
            }
            catch
            {
                return;
            }
    
            foreach (DirectoryInfo dirChild in dirs)
                Items.Add(new DirectoryTreeViewItem(dirChild));
        }
        // Event override to populate subitems.
        // Событие отменяется для заполнения подэлементов.
        protected override void OnExpanded(RoutedEventArgs args)
        {
            base.OnExpanded(args);

            foreach (object obj in Items)
            {
                DirectoryTreeViewItem item = obj as DirectoryTreeViewItem;
                item.Populate();
            }
        }
    }
}
