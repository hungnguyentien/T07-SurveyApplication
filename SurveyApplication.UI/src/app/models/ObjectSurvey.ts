import { Representative } from "./Representative"

export interface ObjectSurvey{
    MaDonVi: string
    MaLoaiHinh :number
    MaLinhVuc:number
    TenDonVi:string
    DiaChi:string
    MaSoThue:string
    Email:string
    WebSite:string
    SoDienThoai:string
    listRepresentative:Representative
}
