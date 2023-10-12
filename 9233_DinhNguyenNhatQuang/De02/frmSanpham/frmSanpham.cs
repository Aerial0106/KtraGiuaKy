using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Models;
using BLL;

namespace frmSanpham
{
    public partial class frmSanpham : Form
    {
        private readonly SanPhamBLL sanPhamBLL = new SanPhamBLL();
        private readonly LoaiSanPham loaiSanPham = new LoaiSanPham();

        public frmSanpham()
        {
            InitializeComponent();
        }
        private void btThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát?", "thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                this.Close();
        }

        private void Fill_CboLoaiSP(List<LoaiSanPham> listLoaiSP)
        {
            this.cboLoaiSP.DataSource = listLoaiSP;
            this.cboLoaiSP.DisplayMember = "TenLoai";
            this.cboLoaiSP.ValueMember = "MaLoai";
        }

        private void BindListView(List<SanPham> listSP)
        {
            lvSanpham.Items.Clear();
            foreach (var item in listSP)
            {
                ListViewItem listViewItem = new ListViewItem(item.MaSP);
                listViewItem.SubItems.Add(item.TenSP);
                listViewItem.SubItems.Add(item.Ngaynhap.ToString());
                listViewItem.SubItems.Add(item.LoaiSanPham.TenLoai);
                lvSanpham.Items.Add(listViewItem);
            }
        }

        private void frmSanpham_Load(object sender, EventArgs e)
        {
            QLSanPhamContextDB contextDB = new QLSanPhamContextDB();
            List<SanPham> sanPhamList = contextDB.SanPham.ToList();
            List<LoaiSanPham> loaiSPList = contextDB.LoaiSanPham.ToList();
            try
            {
                BindListView(sanPhamList);
                Fill_CboLoaiSP(loaiSPList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            try
            {
                QLSanPhamContextDB contextDB = new QLSanPhamContextDB();
                if (txtTenSP.Text == "" || txtMaSP.Text == "")
                    throw new Exception("Hãy điền đầy đủ thông tin!");
                SanPham checkMaSP = contextDB.SanPham.FirstOrDefault(sp => sp.MaSP == txtMaSP.Text);
                if (checkMaSP != null)
                    throw new Exception("Mã SP đã tồn tại");

                string selectedMaLoai = cboLoaiSP.Text;
                LoaiSanPham selectedIDObj = contextDB.LoaiSanPham.FirstOrDefault(NV => NV.TenLoai == selectedMaLoai);
                string maLoai = selectedIDObj.MaLoai;

                SanPham sanPham = new SanPham()
                {
                    MaSP = txtMaSP.Text,
                    TenSP = txtTenSP.Text,
                    Ngaynhap = Convert.ToDateTime(dtNgaynhap.Value),
                    MaLoai = maLoai
                };

                contextDB.SanPham.Add(sanPham);
                contextDB.SaveChanges();

                List<SanPham> sanPhamList = contextDB.SanPham.ToList();
                lvSanpham.Items.Clear();
                BindListView(sanPhamList);
                txtMaSP.Text = "";
                txtTenSP.Text = "";
                throw new Exception("Thêm dữ liệu thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            QLSanPhamContextDB ContextDB = new QLSanPhamContextDB();
            DialogResult dr = MessageBox.Show("Bạn có muốn xóa dữ liệu này?", "thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                SanPham dbDeltete = ContextDB.SanPham.FirstOrDefault(nv => nv.MaSP == txtMaSP.Text);
                if (dbDeltete != null)
                {
                    ContextDB.SanPham.Remove(dbDeltete);
                    ContextDB.SaveChanges();
                    List<SanPham> listNewSP = ContextDB.SanPham.ToList();
                    lvSanpham.Items.Clear();
                    BindListView(listNewSP);
                    txtMaSP.Text = "";
                    txtTenSP.Text = "";
                    MessageBox.Show("Xóa dữ liệu thành công", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void lvSanpham_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            QLSanPhamContextDB ContextDB = new QLSanPhamContextDB();
            try
            {
                string selectedMaLoai = cboLoaiSP.Text;
                LoaiSanPham selectedIDObj = ContextDB.LoaiSanPham.FirstOrDefault(NV => NV.TenLoai == selectedMaLoai);
                string maLoai = selectedIDObj.MaLoai;

                SanPham dbUpdate = ContextDB.SanPham.FirstOrDefault(nv => nv.MaSP == txtMaSP.Text);
                if (dbUpdate != null)
                {
                    dbUpdate.TenSP = txtTenSP.Text;
                    dbUpdate.Ngaynhap = Convert.ToDateTime(dtNgaynhap.Text);
                    dbUpdate.MaLoai = maLoai;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lvSanpham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSanpham.SelectedItems.Count > 0)
            {
                ListViewItem lv = lvSanpham.SelectedItems[0];
                txtMaSP.Text = lv.SubItems[0].Text;
                txtTenSP.Text = lv.SubItems[1].Text;
                dtNgaynhap.Text = lv.SubItems[2].Text;
                cboLoaiSP.Text = lv.SubItems[3].Text;
            }
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            SanPham sanPham = sanPhamBLL.FindName(txtFind.Text);
            ListViewItem listViewItem = new ListViewItem(sanPham.MaSP);
            listViewItem.SubItems.Add(sanPham.TenSP);
            listViewItem.SubItems.Add(sanPham.Ngaynhap.ToString());
            listViewItem.SubItems.Add(sanPham.LoaiSanPham.TenLoai);
            lvSanpham.Items.Clear();
            lvSanpham.Items.Add(listViewItem);
            MessageBox.Show("Tìm thấy dữ liệu!");
        }
    }
}
