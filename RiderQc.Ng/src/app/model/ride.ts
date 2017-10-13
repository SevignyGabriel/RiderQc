import { Comment } from '../model/comment';
import { User } from '../model/user';
import { Level } from '../model/level';

export class Ride {
  RideId: number;
  Title: string ;
  Description: string ;
  CreatorId: number ;
  TrajetId: number ;
  LevelId: number ;
  DateDepart: Date ;
  DateFin: Date; 
  User: User;
  Level: Level;
  Comments: Comment[];

  constructor(RideId?: number, Title?: string, Description?: string, CreatorId?: number, TrajetId?: number, LevelId?: number, DateDepart?: Date, DateFin?: Date) {
      this.RideId = RideId;
      this.Title = Title;
      this.Description = Description;
      this.CreatorId = CreatorId;
      this.TrajetId = TrajetId;
      this.LevelId = LevelId;
      this.DateDepart = DateDepart;
      this.DateFin = DateFin;
  }
}
