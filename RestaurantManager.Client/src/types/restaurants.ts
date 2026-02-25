export type Restaurant = {
  id: string;
  name: string;
  description: string;
  status: "Open" | "Closed";
  cuisine: string;
  location: string;
  imgUrl: string|null;
  ownerId: string|null;
};

export type RestaurantStatus = "Open" | "Closed";