export interface Role {
  id: string;
  name: string;
}

export interface MatrixPermission {
  module: number;
  nameModule: string;
  lstPermission: LstPermission[];
}

export interface LstPermission {
  name: string;
  value: number;
}

export interface CreateUpdateRole {
  id: string;
  name: string;
  matrixPermission: MatrixPermission[];
}
