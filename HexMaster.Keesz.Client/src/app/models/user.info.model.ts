export default class UserInfoDto {
  id: string;
  name: string;
  email: string;

  constructor() {
    this.name = '';
    this.email = '';
  }

  static generateMockTodo(): UserInfoDto {
    return {
      id: 'new',
      name: '',
      email: ''
    };
  }
}
