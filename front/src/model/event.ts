export interface Event {
  id: number;
  title: string;
  date: Date;
  startTime: Date;
  endTime: Date;
  description: string;
  ownerId: number
}

export interface State {
  events: Event[];
}
