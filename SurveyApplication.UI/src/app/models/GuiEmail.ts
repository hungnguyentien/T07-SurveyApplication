import { Base } from "./Common/Base";

export interface CreateGuiEmail {
    lstBangKhaoSat: number[];
    lstIdDonVi: number[];
    tieuDe: string;
    noiDung: string;
}

export interface GuiEmail extends Base {
    maGuiEmail: string;
    diaChiNhan: string;
    tieuDe: string;
    noiDung: string;
    trangThai: number;
    thoiGian: string;
    idBangKhaoSat: number;
    tenBangKhaoSat: string;
    idDonVi: number;
}

export interface GuiEmailBks {
    idBangKhaoSat: number;
    maBangKhaoSat: string;
    tenBangKhaoSat: string;
    countSendEmail: number;
    countSendThanhCong: number;
    countSendLoi: number;
    countSendThuHoi: number;
    ngayBatDau: Date | null;
    ngayKetThuc: Date | null;
}