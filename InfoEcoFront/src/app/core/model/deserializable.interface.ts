export interface IDeserializable<T> {
  deserialize(input: any): T;
}
