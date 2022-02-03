export class TrainingSessionModel {
    public id: number | null = null;
    public training_id: number | null = null;
    public location_id: number | null = null;
    public date = '';
    public price: number | null = null;
    public minutes: number | null = null;
    public min_member: number | null = null;
    public max_member: number | null = null;
    public number_of_applicants: number  = 0;
    public address_name = '';
    public place_name = '';
    
}