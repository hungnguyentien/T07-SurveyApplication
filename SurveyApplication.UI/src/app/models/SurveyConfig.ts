export interface Cot {
    MaCot: string;
    Noidung: string;
}

export interface Hang {
    MaHang: string;
    Noidung: string;
}


export interface SurveyConfig {
    LoaiCauHoi: number;
    MaCauHoi: string;
    BatBuoc?: boolean;
    TieuDe: string;
    IsOther?: boolean;
    LabelCauTraLoi: string;
    KichThuocFile: number;
    LstCot: Cot[];
    LstHang: Hang[];
}
  