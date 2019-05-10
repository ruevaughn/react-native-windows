import { StyleSheet } from 'react-native'
import CommonStyles from '../styles'

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
  autoCapOrdinar: {
    backgroundColor: 'lightyellow'
  },
  autoCapSelected: {
    borderWidth: 1,
    borderColor: 'maroon',
  },
  textInput: {
    flexDirection: 'row',
    justifyContent: 'flex-start',
    alignItems: 'flex-start',
    borderWidth: 1,
    borderColor: 'blanchedalmond',
    margin: 2
  },
  imageBox: {
    height: 32,
    width: 46
  },
  outTouchableOpacityStyle: {
    flex: 1,
    marginBottom: 5,
    height: '100%',
    borderBottomLeftRadius: 22,
    borderTopLeftRadius: 22,
    backgroundColor: '#0070A0'
  },
  innerViewStyle: {
    flex: 1,
    height: '100%',
    borderBottomLeftRadius: 22,
    borderTopLeftRadius: 22,
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: 'green',
  }
})