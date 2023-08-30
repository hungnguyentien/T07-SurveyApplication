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
  id: number;
}

export interface CreateNguoiDaiDien {
  hoTen: string;
  chucVu: string;
  soDienThoai: string;
  email: string;
  id: number;
}

export interface GeneralInfo {
  donVi: CreateDonVi;
  nguoiDaiDien: CreateNguoiDaiDien;
}
