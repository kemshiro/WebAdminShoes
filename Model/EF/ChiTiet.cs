namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTiet")]
    public partial class ChiTiet
    {
        [Key]
        public int maCT { get; set; }

        public int maHD { get; set; }

        public int maSP { get; set; }

        public int soLuong { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
