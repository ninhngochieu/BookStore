export class Category {
  constructor(name: string) {
    this.name = name;
  }
  
  avatar_sm: string;
  id: number;
  indent_name: string;
  is_leaf: boolean;
  name: string;
  parent_id: number;
  slug: string;
}
