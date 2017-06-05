namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTiets = new HashSet<ChiTiet>();
        }

        [Key]
        public int maSP { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Tên sản phẩm")]
        public string tenSP { get; set; }

        [Column(TypeName = "money")]
        [Display(Name = "Đơn Giá")]
        public decimal Gia { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Thông tin")]
        public string Ttsp { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Đường dẫn ảnh")]
        public string Image { get; set; }

        public int maTL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTiet> ChiTiets { get; set; }

        [Display(Name = "Loại sản phẩm")]
        public virtual TheLoai TheLoai { get; set; }
    }
}
