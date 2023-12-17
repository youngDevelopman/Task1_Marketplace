export interface Product {
  id: number;
  name: string;
  price: number;
  description: string;
  image: string;
  addedBy: {
    id: string;
    name: string;
  }
  rating: number;
}
