export default class Product {
  constructor(value: Partial<Product>) {
    Object.assign(this, value);
  }
  name: string | null = null;
  image: string | null = null;
}
