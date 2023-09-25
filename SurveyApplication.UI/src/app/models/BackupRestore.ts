export interface BackupRestore {
  id: number;
  name: string;
  capacity: string;
  timeBackup: string;
  createDateTime: string;
}

export interface ConfigJobBackup {
  scheduleDayofweek: number;
  scheduleHour: number;
  scheduleMinute: number;
}
