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

export interface GeneralInfo {
  createDonVi: CreateDonVi;
}
