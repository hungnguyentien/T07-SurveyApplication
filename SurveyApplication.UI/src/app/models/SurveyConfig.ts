export interface Cot {
    maCot: string;
    noidung: string;
}

export interface Hang {
    maHang: string;
    noidung: string;
}


export interface SurveyConfig {
    loaiCauHoi: number;
    maCauHoi: string;
    batBuoc?: boolean;
    tieuDe: string;
    isOther?: boolean;
    labelCauTraLoi: string;
    kichThuocFile: number;
    lstCot: Cot[];
    lstHang: Hang[];
    noidung: string;
}
