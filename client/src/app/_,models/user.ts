import { Announcement } from './announcement';
export interface User{
  id:number;
  username:string;
  email:string;
  dormitotiNumber: number;
  dormitotiRoom: number;
  token: string;
  announcements: Announcement[];
}
