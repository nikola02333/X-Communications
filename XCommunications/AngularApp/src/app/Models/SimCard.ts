export class SimCard {

    constructor(
        public imsi: number,
        public iccid: number,
        public pin: number,
        public puk: number,
        // public status: boolean
    ) { }
}