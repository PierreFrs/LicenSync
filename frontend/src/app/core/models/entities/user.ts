export type User = {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  userInfo: UserInfo;
}

export type UserInfo = {
  phoneNumber: string;
  birthDate: string;
  profilePicturePath: string;
  address: string;
  city: string;
  country: string;
  postalCode: string;
  userName: string;
}


