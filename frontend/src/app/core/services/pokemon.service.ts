import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Pokemon } from '../models/pokemon.model';

@Injectable({
  providedIn: 'root'
})
export class PokemonService {

  private readonly apiUrl = 'http://localhost:5049/api/pokemon';

  constructor(private http: HttpClient) {}

  getPokemons(
    limit: number,
    offset: number,
    search: string = ''
  ): Observable<Pokemon[]> {

    let params = new HttpParams()
      .set('limit', limit)
      .set('offset', offset);

    if (search) {
      params = params.set('search', search);
    }

    return this.http.get<Pokemon[]>(this.apiUrl, { params });
  }
}
