import { Component, OnInit,ChangeDetectorRef  } from '@angular/core';
import { CommonModule } from '@angular/common';
import { finalize } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { PokemonService } from '../../core/services/pokemon.service';
import { Pokemon } from '../../core/models/pokemon.model';

@Component({
  selector: 'app-pokemon',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pokemon.html'
})
export class PokemonComponent implements OnInit {

  pokemons: Pokemon[] = [];
  loading = false;
  error = '';
  limit = 10;
  offset = 0;
  search = '';

  constructor(
    private pokemonService: PokemonService,
    private cdr: ChangeDetectorRef
  ) {}


  ngOnInit(): void {
    this.loadPokemons();
  }

  loadPokemons(): void {
    this.loading = true;
    this.error = '';

    this.pokemonService
      .getPokemons(this.limit, this.offset, this.search)
      .pipe(
        finalize(() => {
          this.loading = false;
          this.cdr.detectChanges(); // También aquí para el loading
        })
      )
      .subscribe({
        next: (data) => {
          console.log('Pokemons:', data);
          this.pokemons = data;
          this.cdr.detectChanges();
        },
        error: () => {
          this.error = 'Error cargando Pokémon';
          this.cdr.detectChanges();
        }
      });
  }


  onSearch(): void {
    this.offset = 0; // MUY importante
    this.loadPokemons();
  }


  nextPage(): void {
    this.offset += this.limit;
    this.loadPokemons();
  }

  prevPage(): void {
    if (this.offset >= this.limit) {
      this.offset -= this.limit;
      this.loadPokemons();
    }
  }
}
