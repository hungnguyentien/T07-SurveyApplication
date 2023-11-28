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
  idTinhTp: number;
  idQuanHuyen: number;
  idXaPhuong: number;
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
  trangThaiKhaoSat: number;
  trangThaiKq: number;
  data: string | undefined;
}

export interface LoaiHinhDonVi {
  id: string;
  tenLoaiHinh: string;
}

export interface HanhChinhVn {
  id: number;
  name: string;
  type: string;
  slug: string;
  name_with_type: string;
  path: string;
  path_with_type: string;
  code: string;
  parent_code: string;
}
