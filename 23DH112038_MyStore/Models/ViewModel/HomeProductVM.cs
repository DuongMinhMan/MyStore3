﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _23DH112038_MyStore.Models;
using PagedList.Mvc;

namespace _23DH112038_MyStore.Models.ViewModel
{
    public class HomeProductVM
    {
        //tiêu chí đẻ search theo tên, mô tả sp hoặc loại sản phẩm
        public string SearchTerm { get; set; }
        //các thuộc tính hỗ trợ phân trang
        public int PageNumber { get; set; }//trang hiện tại
        public int PageSize { get; set; } = 10; //Số sản phẩm mỗi trang
        //danh sách sản phẩm nổi bật
        public List<Product> FeaturedProducts { get; set; }
        //danh sách sản phẩm mới đã phân trang
        public PagedList.IPagedList<Product> NewProducts { get; set; }
    }
}