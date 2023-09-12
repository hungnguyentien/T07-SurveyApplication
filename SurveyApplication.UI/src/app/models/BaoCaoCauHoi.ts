export interface CreateBaoCaoCauHoi {
    idCauHoi: number;
    idDotKhaoSat: number;
    idBangKhaoSat: number;
    idLoaiHinhDonVi: number;
    maCauHoi: string | null;
    cauHoi: string | null;
    maCauHoiPhu: string | null;
    cauHoiPhu: string | null;
    maCauTraLoi: string | null;
    cauTraLoi: string | null;
    loaiCauHoi: number;
    tenDaiDienCq: string | null;
    dauThoiGian: string | null;
}