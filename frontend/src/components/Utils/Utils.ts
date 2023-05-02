import styled from "styled-components";

export const Wrapper = styled.div`
  box-sizing: border-box;
  padding: 30px 100px;
  background-color: #ffffff;
`;

export const VerticalMargins = styled.div`
  margin: 30px 0px;
`;

export const HorizontalGrid = styled.div`
  display: grid;
  grid-gap: 20px;
  grid-auto-flow: column;
`;

export const VerticalGrid = styled.div`
  display: grid;
  grid-gap: 20px;
`;

export const Card = styled.div`
  background-color: #f6f8fc;
  border-radius: 8px;
  padding: 20px;
`;

export const AlignCenter = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
`;
