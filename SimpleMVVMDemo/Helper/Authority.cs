using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleMVVMDemo.Helper
{
    public class Authority
    {
        public static List<Visibility> AuthorityOperating(int authority, int count)
        {
            var visibilities = new List<Visibility>();
            for (int i = 0; i <= count; i++)
            {
                int temp = 1 << i;
                visibilities.Add((authority & temp) == temp ? Visibility.Visible : Visibility.Collapsed);
            }
            return visibilities;
        }
    }
}
