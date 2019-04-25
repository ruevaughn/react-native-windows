import { StyleSheet } from 'react-native'

export default StyleSheet.create({
  content: {
    flexGrow: 1,
    flexDirection: 'column',
    backgroundColor: 'lightyellow',
    borderWidth: 1,
    borderColor: 'blanchedalmond'
  },
  item: {
    flex: 1
  },
  title: {
    fontSize: 25
  },
  caption: {
    fontSize: 20
  },
  subCaption: {
    fontSize: 16,
    fontWeight: '500'
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
  section: {
    height: 100,
    backgroundColor: '#0070A0',
    borderWidth: 2,
    borderColor: '#505050',
  },
  expandToContainer: {
    flex: 1,
    width: 'auto'
  },
  moreAreaMedium: {
    width: 32,
    height: '100%',
    borderBottomRightRadius: 17,
    borderTopRightRadius: 17
  },
  mainAreaMedium: {
    flex: 1,
    height: '100%',
    borderBottomLeftRadius: 17,
    borderTopLeftRadius: 17
  },
})
