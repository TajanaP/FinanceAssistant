export interface Type {
    id: number;
    name: string;
}

export interface Category {
    id: number;
    name: string;
    type: Type;
}

export interface Transaction {
    id: number;
    category: Category;
    description: string;
    amount: number;
    currency: string;
    date: Date;
}

export interface SaveTransaction {
    id: number;
    typeId: number;
    categoryId: number;
    description: string;
    amount: number;
    currency: string;
    date: Date;
}