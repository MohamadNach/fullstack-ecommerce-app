import { ChangeEvent } from 'react';

export type SortType = {
  sortCriteria: string;
  onHandleSortChange: (e: ChangeEvent<HTMLSelectElement>) => void;
};
