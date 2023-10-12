using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SanPhamBLL
    {
        public List<SanPham> GetAll()
        {
            QLSanPhamContextDB contextDB = new QLSanPhamContextDB();
            return contextDB.SanPham.ToList();
        }

        public SanPham FindName(string Name)
        {
            QLSanPhamContextDB contextDB =new QLSanPhamContextDB();
            return contextDB.SanPham.FirstOrDefault(sp => sp.TenSP == Name);
        }
    }
}
