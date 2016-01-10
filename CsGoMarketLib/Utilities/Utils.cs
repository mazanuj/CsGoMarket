using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using CsGoMarketLib.DataTypes;

namespace CsGoMarketLib.Utilities
{
    public static class Utils
    {
        static Utils()
        {
            ItemsCollection = new List<ItemStruct>();
            SellCollection = new ObservableCollection<ItemStruct>();
            BuyCollection = new ObservableCollection<ItemStruct>();
        }

        public static bool IsPermit { get; set; }
        public static string SecretKey { get; set; }
        public static List<ItemStruct> ItemsCollection { get; set; }
        public static ObservableCollection<ItemStruct> SellCollection { get; set; }
        public static ObservableCollection<ItemStruct> BuyCollection { get; set; }

        public static T FindChild<T>(DependencyObject parent, string childName)
            where T : DependencyObject
        {
            // Confirm parent is valid.  
            if (parent == null) return null;

            T foundChild = null;

            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child 
                var childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree 
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child.  
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search 
                    if (frameworkElement == null || frameworkElement.Name != childName) continue;
                    // if the child's name is of the request name 
                    foundChild = (T) child;
                    break;
                }
                else
                {
                    // child element found. 
                    foundChild = (T) child;
                    break;
                }
            }
            return foundChild;
        }
    }
}