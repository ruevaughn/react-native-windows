import { StyleSheet } from 'react-native'
import CommonStyles from '../styles'

const box = {
  position: 'absolute',
  zIndex: 1,
  width: 40,
  height: 40,
  borderWidth: 2,
  borderColor: 'black'
}


export default StyleSheet.create({
  content: {
    ...CommonStyles.content
  },
  item: {
    ...CommonStyles.item
  },
  title: {
    ...CommonStyles.section.title
  },
  caption: {
    ...CommonStyles.section.caption
  },
  subCaption: {
    ...CommonStyles.section.subCaption
  },
  testBar: {
    flexDirection: 'row',
    justifyContent: 'flex-start',
    alignItems: 'flex-start',
    borderWidth: 1,
    borderColor: 'blanchedalmond',
    margin: 2,
    height: 90
  },
  greenBox: {
    ...box,
    top: 40,
    right: 50,
    backgroundColor: 'green'
  },
  redBox: {
    ...box,
    top: 20,
    right: 30,
    backgroundColor: 'red'
  },
  borderWidth: {
    width: 40,
    height: 40,
    borderLeftWidth: 5,
    borderRightWidth: 8,
    borderTopWidth: 2,
    borderBottomWidth: 12,
    borderColor: 'black',
    backgroundColor: 'green'
  },
  borderColor: {
    width: 40,
    height: 40,
    borderWidth: 10,
    borderLeftColor: 'yellow',
    borderRightColor: 'blue',
    borderBottomColor: 'white',
    borderTopColor: 'pink',
    backgroundColor: 'green'
  },
  borderStartWidth: {
    width: 80,
    height: 40,
    borderStartWidth: 5,
    borderLeftWidth: 12,
    borderRightWidth: 2,
    borderTopWidth: 16,
    borderBottomWidth: 4,
    borderColor: 'black',
    backgroundColor: 'green',
    direction: 'rtl'
  },

})