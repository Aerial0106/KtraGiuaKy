using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LoaiSanPhamBLL
    {
        public List<LoaiSanPham> GetAll()
        {
            QLSanPhamContextDB contextDB = new QLSanPhamContextDB();
            return contextDB.LoaiSanPham.ToList();
        }

        public LoaiSanPham FindID(string ID)
        {
            QLSanPhamContextDB contextDB= new QLSanPhamContextDB();
            return contextDB.LoaiSanPham.FirstOrDefault(sp => sp.MaLoai == ID);
        }
    }
}
