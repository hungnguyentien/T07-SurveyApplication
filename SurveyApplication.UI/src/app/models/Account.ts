import { MatrixPermission } from './Role';

export interface Account {
  id: string;
  name: string | null;
  userName: string | null;
  email: string | null;
  address: string | null;
}

export interface Register {
  id: string;
  address: string;
  name: string;
  email: string;
  userName: string;
  password: string;
  passwordConfirmed: string;
  lstRoleName: string[];
  matrixPermission: MatrixPermission[] | null;
}
