export interface CreateDonVi {
  maDonVi: string;
  maLoaiHinh: number;
  maLinhVuc: number;
  tenDonVi: string;
  diaChi?: string;
  maSoThue: string;
  email: string;
  webSite: string;
  soDienThoai: string;
}

export interface CreateNguoiDaiDien {
  hoTen: string;
  chucVu: string;
  soDienThoai: string;
  email: string;
}

export interface GeneralInfo {
  donVi: CreateDonVi;
  nguoiDaiDien: CreateNguoiDaiDien;
}
