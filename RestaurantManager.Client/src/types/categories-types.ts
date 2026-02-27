export type Category = {
    id: string,
    name: string,
    isActive: boolean
}

export type ListAllCategoriesApiResponse = {
    categories: Category[]
}
