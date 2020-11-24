import Music from './music';

export default class Album{
    public id?: string;
    public name?: string;
    public description?: string;
    public backdrop?: string;
    public band?: string;
    public musics?: Music[];
}
