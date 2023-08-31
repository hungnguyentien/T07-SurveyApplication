
export interface CreateUnitAndRep{
    DonViDto:ObjectSurvey
    NguoiDaiDienDto:Representative
}

export interface ObjectSurvey{
    MaLoaiHinh :number
    MaLinhVuc:number
    TenDonVi:string
    DiaChi:string
    MaSoThue:string
    Email:string
    WebSite:string
    SoDienThoai:string
   
}
export interface Representative{
    SoDienThoai:string
    HoTen:string
    ChucVu:string
    Email:string
    MoTa:string
}