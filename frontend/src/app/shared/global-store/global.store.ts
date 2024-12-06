import { computed } from '@angular/core';
import {
  patchState,
  signalStore,
  withComputed,
  withMethods,
  withState,
} from '@ngrx/signals';

type GlobalState = {
  language: string;
  logo: string;
};

const initialState: GlobalState = {
  language: '',
  logo: `../../../assets/logo-{language}.png`,
};

export const GlobalStore = signalStore(
  { providedIn: 'root' },
  withState(initialState),
  withComputed((store) => ({
    getLogo: computed(() =>
      store.logo().includes('{language}')
        ? store.logo().replace('{language}', store.language())
        : store.logo()
    ),
  })),
  withMethods((store) => ({
    setLanguage(language: string): void {
      patchState(store, { language });
    },
  }))
);
