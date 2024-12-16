using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace WindowsFormsApp1
{
    public partial class frmSanpham : Form
    {
        public frmSanpham()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            // Khai báo và gán giá trị chuỗi kết nối
            string connectionString = "Data Source=your_server_name;Initial Catalog=your_database_name;Integrated Security=True";

            // Giả sử bạn đã thiết lập kết nối và truy vấn SQL
            string query = "SELECT MaSP, TenSP, LoaiSP FROM Products";

            // Tạo kết nối và truy vấn dữ liệu
            SqlDataAdapter adapter = new SqlDataAdapter(query, connectionString);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Gán dữ liệu vào DataGridView
            dataGridViewSanPham.DataSource = dataTable;
        }

        private void dtdataGridViewSanPham()
        {
            using (var context = new Model1())
            {
                var products = context.Products.ToList();  // Lấy tất cả sản phẩm từ cơ sở dữ liệu

                dataGridViewSanPham.Rows.Clear();  // Xóa các dòng hiện tại trong DataGridView

                foreach (var product in products)
                {
                    dataGridViewSanPham.Rows.Add(
                        product.ProductCode,  // Mã sản phẩm
                        product.ProductName,  // Tên sản phẩm
                        product.EntryDate.ToShortDateString(),  // Ngày nhập
                        product.Category  // Loại sản phẩm
                    );
                }
            }
        }
        public class Product
        {
            public int ProductID { get; set; } // Khoá chính, có thể tự động sinh
            public string ProductCode { get; set; }
            public string ProductName { get; set; }
            public DateTime EntryDate { get; set; }
            public string Category { get; set; }
        }

        public class Model1 : DbContext
        {
            // Tạo DbSet<Product> để đại diện cho bảng Products trong cơ sở dữ liệu
            public DbSet<Product> Products { get; set; }

            // Bạn có thể cấu hình chuỗi kết nối hoặc các thiết lập khác trong constructor
            public Model1() : base("name=YourConnectionString") // Sử dụng tên chuỗi kết nối của bạn
            {
            }
        }

        private void LoadDataGridView()
        {
            using (var context = new Model1())
            {
                // Lấy tất cả sản phẩm từ cơ sở dữ liệu
                var products = context.Products.ToList();

                // Xóa tất cả các dòng hiện tại trong DataGridView
                dataGridViewSanPham.Rows.Clear();

                // Thêm các sản phẩm vào DataGridView
                foreach (var product in products)
                {
                    dataGridViewSanPham.Rows.Add(
                        product.ProductCode,  // Mã sản phẩm
                        product.ProductName,  // Tên sản phẩm
                        product.EntryDate.ToShortDateString(),  // Ngày nhập
                        product.Category  // Loại sản phẩm
                    );
                }
            }
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dtg_sinhvien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridViewSanPham.Rows[e.RowIndex];

                // Kiểm tra cột "MaSP" có tồn tại hay không
                if (selectedRow.Cells["MaSP"] != null)
                {
                    txtMaSP.Text = selectedRow.Cells["MaSP"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Cột 'MaSP' không tồn tại.");
                }
            }
            dataGridViewSanPham.Columns.Add("MaSP", "Mã Sản Phẩm");
            dataGridViewSanPham.Columns.Add("MaSP", "Mã Sản Phẩm");
            dataGridViewSanPham.Columns.Add("TenSP", "Tên Sản Phẩm");
            dataGridViewSanPham.Columns.Add("NgayNhap", "Ngày Nhập");
            dataGridViewSanPham.Columns.Add("LoaiSP", "Loại Sản Phẩm");
            
        }
        
        private void txt_ma_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_ten_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtNgaynhap_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmb_Loai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text) ||
                string.IsNullOrWhiteSpace(txtTenSP.Text) ||
                string.IsNullOrWhiteSpace(cbLoaiSP.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dataGridViewSanPham.Rows.Add(
                txtMaSP.Text,
                txtTenSP.Text,
                dtpNgayNhap.Value.ToShortDateString(),
                cbLoaiSP.Text
            );

            txtMaSP.Clear();
            txtTenSP.Clear();
            cbLoaiSP.SelectedIndex = -1;
            dtpNgayNhap.Value = DateTime.Now;

            txtMaSP.Focus();
        }

        private void frmSanpham_Load(object sender, EventArgs e)
        {
            
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            if (dataGridViewSanPham.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewSanPham.SelectedRows)
                {
                    // Xóa dòng được chọn
                    dataGridViewSanPham.Rows.Remove(row);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text) ||
        string.IsNullOrWhiteSpace(txtTenSP.Text) ||
        string.IsNullOrWhiteSpace(cbLoaiSP.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var context = new Model1())  // Kết nối đến cơ sở dữ liệu
            {
                // Kiểm tra xem sản phẩm có mã sản phẩm đã nhập đã tồn tại trong cơ sở dữ liệu chưa
                var existingProduct = context.Products.FirstOrDefault(p => p.ProductCode == txtMaSP.Text.Trim());

                if (existingProduct == null)
                {
                    // Nếu sản phẩm chưa tồn tại, thêm mới sản phẩm vào cơ sở dữ liệu
                    var newProduct = new Product
                    {
                        ProductCode = txtMaSP.Text.Trim(),
                        ProductName = txtTenSP.Text.Trim(),
                        Category = cbLoaiSP.Text,
                        EntryDate = dtpNgayNhap.Value
                    };

                    context.Products.Add(newProduct);  // Thêm sản phẩm mới vào DbSet
                    context.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu

                    MessageBox.Show("Sản phẩm đã được thêm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Nếu sản phẩm đã tồn tại, cập nhật thông tin sản phẩm
                    existingProduct.ProductName = txtTenSP.Text.Trim();
                    existingProduct.Category = cbLoaiSP.Text;
                    existingProduct.EntryDate = dtpNgayNhap.Value;

                    context.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu

                    MessageBox.Show("Sản phẩm đã được cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Làm mới lại DataGridView để hiển thị thông tin mới
                LoadDataGridView();  // Gọi phương thức LoadDataGridView để tải lại dữ liệu
            }

            // Xóa dữ liệu trong các ô nhập liệu để chuẩn bị cho lần nhập sau
            txtMaSP.Clear();
            txtTenSP.Clear();
            cbLoaiSP.SelectedIndex = -1;
            dtpNgayNhap.Value = DateTime.Now;  // Đặt lại ngày nhập thành ngày hiện tại

            // Đặt lại con trỏ vào trường mã sản phẩm
            txtMaSP.Focus();
        }

        private void btKLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem DataGridView có dòng dữ liệu không
                if (dataGridViewSanPham.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var context = new Model1()) // Model1 là lớp DbContext của bạn
                {
                    // Duyệt qua từng dòng trong DataGridView
                    foreach (DataGridViewRow row in dataGridViewSanPham.Rows)
                    {
                        if (row.IsNewRow) continue; // Bỏ qua dòng mới (dòng trống cuối cùng)

                        // Tạo đối tượng Product từ các ô trong dòng DataGridView
                        var product = new Product
                        {
                            ProductCode = row.Cells["MaSP"].Value.ToString(), // Tên cột trong DataGridView
                            ProductName = row.Cells["TenSP"].Value.ToString(),
                            EntryDate = DateTime.Parse(row.Cells["NgayNhap"].Value.ToString()), // Giả sử bạn đã format đúng
                            Category = row.Cells["LoaiSP"].Value.ToString()
                        };

                        // Thêm đối tượng Product vào DbContext
                        context.Products.Add(product);
                    }

                    // Lưu các thay đổi vào cơ sở dữ liệu
                    context.SaveChanges();

                    // Thông báo khi lưu thành công
                    MessageBox.Show("Dữ liệu đã được lưu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Sau khi lưu, có thể xóa dữ liệu trên DataGridView (nếu cần)
                    dataGridViewSanPham.Rows.Clear(); // Dọn dẹp DataGridView nếu muốn xóa sau khi lưu
                }
            }
            catch (Exception ex)
            {
                // Nếu có lỗi xảy ra, thông báo lỗi cho người dùng
                MessageBox.Show("Đã xảy ra lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btTim_Click(object sender, EventArgs e)
        {
            string maSP = txtMaSP.Text.Trim();  // Lấy Mã SP từ TextBox
            string tenSP = txtTenSP.Text.Trim();  // Lấy Tên SP từ TextBox
            string loaiSP = cbLoaiSP.Text.Trim();  // Lấy Loại SP từ ComboBox

            // Kiểm tra và thực hiện tìm kiếm dữ liệu
            using (var context = new Model1())  // Mở kết nối đến cơ sở dữ liệu
            {
                var query = context.Products.AsQueryable();  // Lấy danh sách sản phẩm từ cơ sở dữ liệu

                // Nếu có Mã SP, lọc theo Mã SP
                if (!string.IsNullOrEmpty(maSP))
                {
                    query = query.Where(p => p.ProductCode.Contains(maSP));
                }

                // Nếu có Tên SP, lọc theo Tên SP
                if (!string.IsNullOrEmpty(tenSP))
                {
                    query = query.Where(p => p.ProductName.Contains(tenSP));
                }

                // Nếu có Loại SP, lọc theo Loại SP
                if (!string.IsNullOrEmpty(loaiSP))
                {
                    query = query.Where(p => p.Category.Contains(loaiSP));
                }

                // Thực hiện truy vấn và lấy kết quả
                var result = query.ToList();

                // Hiển thị kết quả vào DataGridView
                dataGridViewSanPham.Rows.Clear();  // Xóa tất cả dữ liệu cũ trong DataGridView

                foreach (var product in result)
                {
                    dataGridViewSanPham.Rows.Add(
                        product.ProductCode,
                        product.ProductName,
                        product.EntryDate.ToShortDateString(),
                        product.Category
                    );
                }

                // Nếu không có sản phẩm nào được tìm thấy
                if (result.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy sản phẩm nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            if (dataGridViewSanPham.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy dòng đã chọn
            var selectedRow = dataGridViewSanPham.SelectedRows[0];

            // Điền thông tin từ dòng đã chọn vào các TextBox và ComboBox để người dùng có thể sửa
            txtMaSP.Text = selectedRow.Cells["MaSP"].Value.ToString();  // Giả sử bạn có cột "MaSP"
            txtTenSP.Text = selectedRow.Cells["TenSP"].Value.ToString();  // Giả sử bạn có cột "TenSP"
            cbLoaiSP.Text = selectedRow.Cells["LoaiSP"].Value.ToString();  // Giả sử bạn có cột "LoaiSP"
            dtpNgayNhap.Value = DateTime.Parse(selectedRow.Cells["NgayNhap"].Value.ToString());  // Giả sử bạn có cột "NgayNhap"

            // Cập nhật thông tin vào cơ sở dữ liệu
            using (var context = new Model1())  // Mở kết nối đến cơ sở dữ liệu
            {
                // Tìm sản phẩm trong cơ sở dữ liệu dựa trên mã sản phẩm
                var productCode = txtMaSP.Text.Trim();  // Lấy mã sản phẩm từ TextBox
                var product = context.Products.FirstOrDefault(p => p.ProductCode == productCode);  // Lấy sản phẩm từ cơ sở dữ liệu theo mã sản phẩm

                if (product != null)
                {
                    // Cập nhật thông tin sản phẩm
                    product.ProductName = txtTenSP.Text;
                    product.Category = cbLoaiSP.Text;
                    product.EntryDate = dtpNgayNhap.Value;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    context.SaveChanges();

                    // Hiển thị thông báo thành công
                    MessageBox.Show("Sản phẩm đã được cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật lại DataGridView sau khi sửa
                    dataGridViewSanPham.Rows.Clear();  // Xóa dữ liệu hiện tại trong DataGridView
                    LoadDataGridView();  // Tải lại dữ liệu từ cơ sở dữ liệu vào DataGridView
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm với mã đã nhập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
