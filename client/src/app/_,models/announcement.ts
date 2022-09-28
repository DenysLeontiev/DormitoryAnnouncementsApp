import { Photo } from './photo';
export interface Announcement{
  id:number;
  body:string;
  header: string;
  photos:Photo[];
}
