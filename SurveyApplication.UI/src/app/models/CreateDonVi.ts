export interface CreateDonVi {
  maDonVi: string;
  idLoaiHinh: number;
  idLinhVuc: number;
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
  bangKhaoSat: number;
  trangThai: number;
  data: string | undefined;
}

export interface LoaiHinhDonVi {
  id: string;
  tenLoaiHinh: string;
}
