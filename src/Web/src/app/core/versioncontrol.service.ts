import { Injectable } from '@angular/core';
import { Http, Response} from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class VersionControlService {
    url: string;
    constructor(private http: Http) {
        this.url = "./api";
    }

    getContent(id: number, path: string) {
        return this.http.post(this.url + "/content/", { ChangeSetId: id, Path: path })
            .map((response: Response) => response.text())
            .catch(this.errorHandler);
    }

    getChangeSets(path: string) {
        return this.http.get(this.url + "/changesets?path=" + path)
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }

    private errorHandler(error: Response) {
        console.error(error);
        return Observable.throw(error.json() || "Server error");
    }
}