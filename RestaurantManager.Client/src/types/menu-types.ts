export type Menu = {
  id: string;
  name: string;
  description?: string;
  imgUrl: string;
  type: MenuType;
  isActive: boolean;
};

export type MenuType = "Default" | "Summer" | "Winter" | "Spring" | "Autumn";